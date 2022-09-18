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
        int[] heights = { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
        Console.WriteLine(Trap(heights));ß
    }

    public int Trap(int[] height) {
        int[] waterTrap = new int[height.Length];
        int currentTallestFromLeft = 0;
        int currentTallestFromRight = 0;
        for(int leftIndex = 0 ; leftIndex < height.Length ; ++leftIndex)
        {
            //Left to right
            if(currentTallestFromLeft < height[leftIndex])
            {
                currentTallestFromLeft = height[leftIndex];
            }
            else
            {
                waterTrap[leftIndex] = currentTallestFromLeft - height[leftIndex];
            }
        }
        
        for(int rightIndex = height.Length - 1 ; rightIndex >= 0 ; --rightIndex)
        {
            //Right to left
            if(currentTallestFromRight <= height[rightIndex])
            {
                currentTallestFromRight = height[rightIndex];
                waterTrap[rightIndex] = 0;
            }
            else
            {
                waterTrap[rightIndex] = Math.Min(waterTrap[rightIndex], currentTallestFromRight - height[rightIndex]);
            }
        }
        
        return waterTrap.Sum();
    }
}