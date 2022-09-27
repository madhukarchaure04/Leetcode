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
    public string PushDominoes(string dominoes)
    {
        char[] result = Enumerable.Repeat('.', dominoes.Length).ToArray();
        int lastSeenRIndex = -1;
        for(int i = 0 ; i < dominoes.Length ; ++i)
        {
            switch(dominoes[i])
            {
                case '.':
                    break;
                case 'L':
                    if(lastSeenRIndex != -1)
                    {
                        ProcessPair(lastSeenRIndex, i, dominoes, result);
                        lastSeenRIndex = -1;
                    }
                    else
                    {
                        ProcessNonPairedL(i, dominoes, result);
                    }
                    break;
                case 'R':
                    if(lastSeenRIndex != -1)
                    {
                        ProcessNonPairedR(lastSeenRIndex, i, dominoes, result);
                    }
                    lastSeenRIndex = i;
                    break;
            }
        }
        if(lastSeenRIndex != -1)
            ProcessNonPairedR(lastSeenRIndex, result.Length, dominoes, result);
        
        return new string(result);
    }
    private void ProcessPair(int R, int L, string dominoes, char[] result)
    {
        while(R < L)
        {
            result[R++] = 'R';
            result[L--] = 'L';
        }
    }
    
    private void ProcessNonPairedL(int lIndex, string dominoes, char[] result)
    {
        while(lIndex >= 0 && result[lIndex] == '.')
        {
            result[lIndex] = 'L';
            --lIndex;
        }
    }
    
    private void ProcessNonPairedR(int start, int end, string dominoes, char[] result)
    {
        while(start < end && result[start] == '.')
        {
            result[start] = 'R';
            ++start;
        }
    }
}