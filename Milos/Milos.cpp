// Milos.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <iostream>
#include <stack>
#include <vector>

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	unsigned int M;
	cin >> M;

	vector<unsigned int> numbers(M);
	unsigned int max = 0;
	for (unsigned int i = 0; i < M; ++i) {
		cin >> numbers[i];

		if (numbers[i] > max)
			max = numbers[i];
	}

	vector<bool> visited(max + 1, false);

	unsigned int prev = numbers[0];
	visited[prev] = true;
	for (unsigned int i = 1; i < M; ++i) // still have numbers to read
	{
		unsigned int curr = numbers[i];
		if (visited[curr])
		{
			cout << "NO" << endl;
			return 0;
		}
		else
		{
			visited[curr] = true;
		}

		if (curr < prev) { // writing about previous!
			for (unsigned int j = curr; j < prev; j++)
			{
				if (visited[j] == false)
				{
					cout << "NO" << endl;
					return 0;
				}
			}
		}
		
		prev = curr;
	}

	cout << "YES" << endl;

	return 0;
}

