/*
Link: https://leetcode.com/problems/find-k-closest-elements/

Given a sorted integer array arr, two integers k and x, return the k closest integers to x in the array. The result should also be sorted in ascending order.

An integer a is closer to x than an integer b if:

|a - x| < |b - x|, or
|a - x| == |b - x| and a < b
 

Example 1:

Input: arr = [1,2,3,4,5], k = 4, x = 3
Output: [1,2,3,4]
Example 2:

Input: arr = [1,2,3,4,5], k = 4, x = -1
Output: [1,2,3,4]
 

Constraints:

1 <= k <= arr.length
1 <= arr.length <= 104
arr is sorted in ascending order.
-104 <= arr[i], x <= 104
*/

public class Solution {
    public IList<int> FindClosestElements(int[] arr, int k, int x) {
        IList<int> result = new List<int>();
        
        int relativePosition = FindRelativePosition(arr, 0, arr.Length-1, x);
        Console.WriteLine(relativePosition);
        if(relativePosition == -1 || relativePosition == arr.Length)
        {
            int skip = relativePosition == -1 ? 0 : arr.Length - k;
            result = arr.Skip(skip).Take(k).ToList();
        }
        else
        {
            result = FindClosestElements(arr, relativePosition, k, x);
        }
        
        return result;
    }
    
    private int FindRelativePosition(int[] array, int start, int end,int num)
    {
        if(start > end)
            return start == 0 ? end : start;
        int mid = (start + end) / 2;
        if(array[mid] == num)
            return mid;
        else if(array[mid] < num)
            return FindRelativePosition(array, mid + 1, end, num);
        else
            return FindRelativePosition(array, start, mid - 1, num);
    }
    
    public IList<int> FindClosestElements(int[] arr,int relativePosition, int k, int x)
    {
        int left = relativePosition-1;
        int right = relativePosition;
        List<int> result = new List<int>();
        while(result.Count < k)
        {
            int leftDiff = left >= 0 ? Math.Abs(arr[left] - x) : Int32.MaxValue;
            int rightDiff = right < arr.Length ? Math.Abs(arr[right] - x) : Int32.MaxValue;
            if(leftDiff <= rightDiff)
            {
                result.Add(arr[left]);
                --left;
            }
            else
            {
                result.Add(arr[right]);
                ++right;
            }
        }
        result.Sort();
        return result;
    }
}