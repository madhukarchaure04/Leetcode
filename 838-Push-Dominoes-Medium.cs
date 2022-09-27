/*
Link: https://leetcode.com/problems/push-dominoes/

There are n dominoes in a line, and we place each domino vertically upright. In the beginning, we simultaneously push some of the dominoes either to the left or to the right.

After each second, each domino that is falling to the left pushes the adjacent domino on the left. Similarly, the dominoes falling to the right push their adjacent dominoes standing on the right.

When a vertical domino has dominoes falling on it from both sides, it stays still due to the balance of the forces.

For the purposes of this question, we will consider that a falling domino expends no additional force to a falling or already fallen domino.

You are given a string dominoes representing the initial state where:

dominoes[i] = 'L', if the ith domino has been pushed to the left,
dominoes[i] = 'R', if the ith domino has been pushed to the right, and
dominoes[i] = '.', if the ith domino has not been pushed.
Return a string representing the final state.

 

Example 1:
Input: dominoes = "RR.L"
Output: "RR.L"
Explanation: The first domino expends no additional force on the second domino.

Example 2:
https://s3-lc-upload.s3.amazonaws.com/uploads/2018/05/18/domino.png
Input: dominoes = ".L.R...LR..L.."
Output: "LL.RR.LLRRLL.."
 

Constraints:

n == dominoes.length
1 <= n <= 105
dominoes[i] is either 'L', 'R', or '.'.
*/

public class Solution {
    public string PushDominoes(string dominoes) {
        List<int> nonPairedL = new List<int>();
        List<int> nonPairedR = new List<int>();
        Dictionary<int, int> pairsRL = FindPairs(dominoes, nonPairedL, nonPairedR);
        char[] result = Enumerable.Repeat('.', dominoes.Length).ToArray();
        ProcessPairs(pairsRL, dominoes, result);
        ProcessNonPairedL(nonPairedL, dominoes, result);
        ProcessNonPairedR(nonPairedR, dominoes, result);
        return new string(result);
        
    }
    private Dictionary<int, int> FindPairs(string dominoes, List<int> nonPairedL, List<int> nonPairedR)
    {
        Dictionary<int, int> pairsRL = new Dictionary<int, int>();
        
        int currentR = -1;
        for(int i = 0 ; i < dominoes.Length ; ++i)
        {
            switch(dominoes[i])
            {
                case '.':
                    break;
                case 'L':
                    if(currentR != -1)
                    {
                        pairsRL[currentR] = i;
                        currentR = -1;
                    }
                    else
                        nonPairedL.Add(i);
                    break;
                case 'R':
                    if(currentR != -1)
                        nonPairedR.Add(currentR);
                    
                    currentR = i;
                    break;
            }
        }
        if(currentR != -1)
            nonPairedR.Add(currentR);
        
        return pairsRL;
    }
    
    private int FindPairedL(string dominoes,int rIndex)
    {
        for(int i = rIndex + 1 ; i < dominoes.Length ; ++i)
        {
            if(dominoes[i] == 'L')
                return i;
        }
        return -1;
    }
    private void FindNonPairedR(string dominoes,int start, int end,List<int> nonPairedR)
    {
        while(start < end)
        {
            if(dominoes[start] == 'R')
                nonPairedR.Add(start);
            ++start;
        }
    }
    private void ProcessPairs(Dictionary<int, int> pairsRL, string dominoes, char[] result)
    {
        foreach(var item in pairsRL)
        {
            int R = item.Key;
            int L = item.Value;
            result[R] = 'R';
            result[L] = 'L';
            while(++R < --L)
            {
                
                if(dominoes[R] == '.')
                    result[R] = 'R';
                else
                {
                    result[R] = dominoes[R];
                    break;
                }
                if(dominoes[L] == '.')
                    result[L] = 'L';
                else
                    result[L] = dominoes[L];
            }
        }
    }
    private void ProcessNonPairedL(List<int> nonPairedL, string dominoes, char[] result)
    {
        foreach(int item in nonPairedL)
        {
            int i = item;
            while(i >= 0 && result[i] == '.')
            {
                result[i] = 'L';
                --i;
            }
        }
    }
    
    private void ProcessNonPairedR(List<int> nonPairedR, string dominoes, char[] result)
    {
        foreach(int item in nonPairedR)
        {
            int i = item;
            while(i < result.Length && result[i] == '.')
            {
                result[i] = 'R';
                ++i;
            }
        }
    }
}