/*
Link: https://leetcode.com/problems/remove-nth-node-from-end-of-list/

Given the head of a linked list, remove the nth node from the end of the list and return its head.

Example 1:
https://assets.leetcode.com/uploads/2020/10/03/remove_ex1.jpg

Input: head = [1,2,3,4,5], n = 2
Output: [1,2,3,5]
Example 2:

Input: head = [1], n = 1
Output: []
Example 3:

Input: head = [1,2], n = 1
Output: [1]
 

Constraints:

The number of nodes in the list is sz.
1 <= sz <= 30
0 <= Node.val <= 100
1 <= n <= sz

*/

/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode RemoveNthFromEnd(ListNode head, int n) {
        int i = 1;
        ListNode second = head;
        while(i < n)
        {
            second = second.next;
            ++i;
        }
        
        ListNode first = null;
        while(second.next != null)
        {
            first = first == null ? head : first.next;
            second = second.next;
        }
        
        if(first == null)
            return head.next;
        
        first.next = first.next != null ? first.next.next : null;
        
        return head;
    }
}