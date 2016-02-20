using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class Solution
    {
        static void Main(string[] args)
        {
            string[] line = Console.ReadLine().Split();
            int T = int.Parse(line[0]);
            int H = int.Parse(line[1]);

            Dictionary<int, List<int>> hotelTickets = new Dictionary<int, List<int>>(T);
            // ticket#: {hotels}
            for (int t = 0; t <= T; ++t)
            {
                hotelTickets.Add(t, new List<int>());
            }

            for (int t = 0; t < T; ++t)
            {
                List<string> ticketInfo = Console.ReadLine().Split().ToList<string>();
                for (int h = 1; h <= int.Parse(ticketInfo[0]); h++)
                {
                    hotelTickets[t].Add(int.Parse(ticketInfo[h]));
                }
            }
            bool[] hotelsVisited = new bool[H + 1];
            int ticketsUsed = useTickets(hotelsVisited, 0, hotelTickets.OrderBy(c => c.Value.Count()), 0);

            Console.WriteLine(H - ticketsUsed);
        }

        private static int useTickets(bool[] hotelsVisited, int usedTickets, IOrderedEnumerable<KeyValuePair<int, List<int>>> hotelTickets, int currTicket)
        {
            if (currTicket >= hotelTickets.Count())
                return usedTickets;

            int maxUsedTickets = 0;
            foreach(int h in hotelTickets.ElementAt(currTicket).Value) // for each hotel in the ticket
            {
                if(hotelsVisited[h] == false) // not visited
                {
                    hotelsVisited[h] = true; // flag as visited
                    int ticketsUsedPath = useTickets(hotelsVisited, usedTickets + 1, hotelTickets, currTicket + 1);
                    hotelsVisited[h] = false; // unflag as visited (for other recursive calls)
             
                    if (maxUsedTickets < ticketsUsedPath)
                        maxUsedTickets = ticketsUsedPath;
                }
            }

            // also try not using the ticket...
            int dontUse = useTickets(hotelsVisited, usedTickets, hotelTickets, currTicket + 1);
            if (maxUsedTickets < dontUse)
                maxUsedTickets = dontUse;
            
            return maxUsedTickets;
        }
    }
}
