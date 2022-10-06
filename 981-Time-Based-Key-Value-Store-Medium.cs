/*
Link: https://leetcode.com/problems/time-based-key-value-store/

Design a time-based key-value data structure that can store multiple values for the same key at different time stamps and retrieve the key's value at a certain timestamp.

Implement the TimeMap class:

TimeMap() Initializes the object of the data structure.
void set(String key, String value, int timestamp) Stores the key key with the value value at the given time timestamp.
String get(String key, int timestamp) Returns a value such that set was called previously, with timestamp_prev <= timestamp. If there are multiple such values, it returns the value associated with the largest timestamp_prev. If there are no values, it returns "".
 

Example 1:

Input
["TimeMap", "set", "get", "get", "set", "get", "get"]
[[], ["foo", "bar", 1], ["foo", 1], ["foo", 3], ["foo", "bar2", 4], ["foo", 4], ["foo", 5]]
Output
[null, null, "bar", "bar", null, "bar2", "bar2"]

Explanation
TimeMap timeMap = new TimeMap();
timeMap.set("foo", "bar", 1);  // store the key "foo" and value "bar" along with timestamp = 1.
timeMap.get("foo", 1);         // return "bar"
timeMap.get("foo", 3);         // return "bar", since there is no value corresponding to foo at timestamp 3 and timestamp 2, then the only value is at timestamp 1 is "bar".
timeMap.set("foo", "bar2", 4); // store the key "foo" and value "bar2" along with timestamp = 4.
timeMap.get("foo", 4);         // return "bar2"
timeMap.get("foo", 5);         // return "bar2"
 

Constraints:

1 <= key.length, value.length <= 100
key and value consist of lowercase English letters and digits.
1 <= timestamp <= 107
All the timestamps timestamp of set are strictly increasing.
At most 2 * 105 calls will be made to set and get.
*/

public class TimeMap {
    Dictionary<string, BinarySearchTree> map;
    public TimeMap() {
        map = new Dictionary<string, BinarySearchTree>();
    }
    
    public void Set(string key, string value, int timestamp) {
        if(map.ContainsKey(key))
        {
            map[key].Add(value, timestamp);
        }
        else
        {
            map[key] = new BinarySearchTree(value, timestamp);
        }
    }
    
    public string Get(string key, int timestamp) {
        if(map.ContainsKey(key))
        {
            return map[key].Search(timestamp);
        }
        else
        {
            return "";
        }
    }
}

class BinaryTreeNode
{
    public string Value;
    public int Timestamp;
    public BinaryTreeNode Left;
    public BinaryTreeNode Right;
    public BinaryTreeNode(string value, int timestamp)
    {
        this.Timestamp = timestamp;
        this.Value = value;
        Left = null;
        Right = null;
    }
}

class BinarySearchTree
{
    public BinaryTreeNode Root;
    public BinarySearchTree(string value, int timestamp)
    {
        Root = new BinaryTreeNode(value, timestamp);
    }
    
    public void Add(string value, int timestamp)
    {
        Add(Root, value, timestamp);
    }
    
    private void Add(BinaryTreeNode node,string value,int timestamp)
    {
        if(node.Timestamp > timestamp)
        {
            if(node.Left != null)
            {
                Add(node.Left, value, timestamp);
            }
            else
            {
                node.Left = new BinaryTreeNode(value, timestamp);
            }
        }
        else
        {
            if(node.Right != null)
            {
                Add(node.Right, value, timestamp);
            }
            else
            {
                node.Right = new BinaryTreeNode(value, timestamp);
            }
        }
    }
    
    public string Search(int timestamp)
    {
        return Search(Root, timestamp, "");
    }
    
    private string Search(BinaryTreeNode node, int timestamp, string lastCloserValue)
    {
        if(node == null)
            return lastCloserValue;
        if(node.Timestamp == timestamp)
            return node.Value;
        
        if(node.Timestamp > timestamp)
            return Search(node.Left, timestamp, lastCloserValue);
        else
            return Search(node.Right, timestamp, node.Value);
    }
}

/**
 * Your TimeMap object will be instantiated and called as such:
 * TimeMap obj = new TimeMap();
 * obj.Set(key,value,timestamp);
 * string param_2 = obj.Get(key,timestamp);
 */