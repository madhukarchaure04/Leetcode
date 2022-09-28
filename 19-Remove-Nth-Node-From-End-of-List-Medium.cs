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
        ListNode nthNodeFromStart = GetNthFromStart(head, n);
        return RemoveNthFromEnd(head, nthNodeFromStart);
    }
    
    private ListNode GetNthFromStart(ListNode head, int n)
    {
        ListNode node = head;
        int i = 1;
        while(i++ < n)
        {
            node = node.next;
        }
        return node;
    }
    
    private ListNode RemoveNthFromEnd(ListNode head, ListNode nthNodeFromStart)
    {
        ListNode nodeBeforeNth = GetNodeBeforeNth(head, nthNodeFromStart);
        if(nodeBeforeNth == null)
            return head.next;
        
        if(nodeBeforeNth.next != null)
            nodeBeforeNth.next = nodeBeforeNth.next.next;
        
        return head;
    }
    
    private ListNode GetNodeBeforeNth(ListNode head, ListNode nthNodeFromStart)
    {
        ListNode node = nthNodeFromStart;
        ListNode nodeBeforeNth = null;
        while(node.next != null)
        {
            nodeBeforeNth = nodeBeforeNth == null ? head : nodeBeforeNth.next;
            node = node.next;
        }
        return nodeBeforeNth;
    }
}