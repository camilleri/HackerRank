using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class Solution
    {
        class HotelTickets
        {
            public int T, H, bestResult;
            Dictionary<int, List<int>> hotelTickets;
            bool[] hotelsVisited;
            IOrderedEnumerable<KeyValuePair<int, List<int>>> orderedHotelTickets;

            public HotelTickets(int numTickets, int numHotels)
            {
                this.T = numTickets;
                this.H = numHotels;

                hotelTickets = new Dictionary<int, List<int>>(T);

                // ticket#: {hotels}
                for (int t = 0; t <= T; ++t)
                {
                    hotelTickets.Add(t, new List<int>());
                }
                hotelsVisited = new bool[H + 1];
                bestResult = 0;
            }

            internal void addHotelToTicker(int t, int h)
            {
                hotelTickets[t].Add(h);
            }

            internal int solve()
            {
                orderedHotelTickets = hotelTickets.OrderBy(c => c.Value.Count());
                int ticketsUsed = useTickets(0, 0);

                return H - ticketsUsed;
            }

            private int useTickets(int usedTickets, int currTicket)
            {
                if (currTicket >= orderedHotelTickets.Count())
                {
                    if (usedTickets > bestResult)
                        bestResult = usedTickets;

                    return usedTickets;
                }

                if (T - currTicket + usedTickets <= bestResult)
                    return 0; // won't be able to get a better result

                int maxUsedTickets = 0;
                foreach (int h in orderedHotelTickets.ElementAt(currTicket).Value) // for each hotel in the ticket
                {
                    if (hotelsVisited[h] == false) // not visited
                    {
                        hotelsVisited[h] = true; // flag as visited
                        int ticketsUsedPath = useTickets(usedTickets + 1, currTicket + 1);
                        hotelsVisited[h] = false; // unflag as visited (for other recursive calls)

                        if (maxUsedTickets < ticketsUsedPath)
                            maxUsedTickets = ticketsUsedPath;
                    }
                }

                // also try not using the ticket...
                int dontUse = useTickets(usedTickets, currTicket + 1);
                if (maxUsedTickets < dontUse)
                    maxUsedTickets = dontUse;

                return maxUsedTickets;
            }
        }
        static void Main(string[] args)
        {
            string[] line = Console.ReadLine().Split();
            int T = int.Parse(line[0]);
            int H = int.Parse(line[1]);

            HotelTickets ht = new HotelTickets(T, H);

            for (int t = 0; t < T; ++t)
            {
                List<string> ticketInfo = Console.ReadLine().Split().ToList<string>();
                for (int h = 1; h <= int.Parse(ticketInfo[0]); h++)
                {
                    ht.addHotelToTicker(t, int.Parse(ticketInfo[h]));
                }
            }

            Console.WriteLine(ht.solve());
        }
    }
}
