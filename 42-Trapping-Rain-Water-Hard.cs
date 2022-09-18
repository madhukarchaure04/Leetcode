/*
Given n non-negative integers representing an elevation map where the width of each bar is 1, compute how much water it can trap after raining.

Example 1:
Input: height = [0,1,0,2,1,0,1,3,2,1,2,1]
https://assets.leetcode.com/uploads/2018/10/22/rainwatertrap.png
Output: 6
Explanation: The above elevation map (black section) is represented by array [0,1,0,2,1,0,1,3,2,1,2,1]. In this case, 6 units of rain water (blue section) are being trapped.

Example 2:
Input: height = [4,2,0,3,2,5]
Output: 9
 

Constraints:

n == height.length
1 <= n <= 2 * 10^4
0 <= height[i] <= 10^5
*/

public class Solution {
    public static void Main()
    {
        int[] height = { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
        Console.WriteLine(Trap(heights));
    }

    public int Trap(int[] height)
    {
        int[] trappedWater = Enumerable.Repeat(Int32.MaxValue, heights.Length).ToArray();
        CalculateTrappedWater(start:0, end:heights.Length, step:1, heights, trappedWater, IsSmallerThan);
        CalculateTrappedWater(start:heights.Length - 1,end: 0,step: -1, heights, trappedWater , IsGreaterThanOrEqual);
        return trappedWater.Sum();
    }

    private void CalculateTrappedWater(int start, int end, int step, int[] heights, int[] trappedWater ,Func<int, int, bool> loopCheck)
    {
        int currentTallest = 0;
        for(int i = start; loopCheck(i, end); i += step)
        {
            if(currentTallest <= heights[i])
            {
                currentTallest = heights[i];
                trappedWater[i] = 0;
            }
            else
            {
                trappedWater[i] = Math.Min(trappedWater[i], currentTallest - heights[i]);
            }
        }
    }

    public bool IsSmallerThan(int first, int second)
    {
        return first < second;
    }
    
    public bool IsGreaterThanOrEqual(int first, int second)
    {
        return first >= second;
    }
}