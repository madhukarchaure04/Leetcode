/*
336. Palindrome Pairs - Hard

Given a list of unique words, return all the pairs of the distinct indices (i, j) in the given list, so that the concatenation of the two words words[i] + words[j] is a palindrome.

Example 1:

Input: words = ["abcd","dcba","lls","s","sssll"]
Output: [[0,1],[1,0],[3,2],[2,4]]
Explanation: The palindromes are ["dcbaabcd","abcddcba","slls","llssssll"]
Example 2:

Input: words = ["bat","tab","cat"]
Output: [[0,1],[1,0]]
Explanation: The palindromes are ["battab","tabbat"]
Example 3:

Input: words = ["a",""]
Output: [[0,1],[1,0]]
 

Constraints:

1 <= words.length <= 5000
0 <= words[i].length <= 300
words[i] consists of lower-case English letters.
*/

public class Solution {
    public IList<IList<int>> PalindromePairs(string[] words) 
    {
        Trie trie = new Trie();   
        AddWordsToTrie(trie, words);
        return GetPalindromePairs(trie, words);
    }
    
    private void AddWordsToTrie(Trie trie,string[] words)
    {
        for(int i = 0 ; i <  words.Length ; ++i)
        {
            trie.AddWord(words[i], i);
        }
    }
    
    private IList<IList<int>> GetPalindromePairs(Trie trie, string[] words)
    {
        IList<IList<int>> palindromePairs = new List<IList<int>>();
        for(int firstIndex = 0 ; firstIndex < words.Length ; ++firstIndex)
        {
            string word = words[firstIndex];
            List<int> secondIndexes = new Helper().GetPalindromeIndexes(trie.GetRoot(), word);
            
            foreach(int secondIndex in secondIndexes)
            {
                if(firstIndex != secondIndex)
                {
                    palindromePairs.Add(GetPair(firstIndex, secondIndex));
                }
            }
        }
        return palindromePairs;
    }
    
    private IList<int> GetPair(int firstIndex, int secondIndex) => new List<int>(){firstIndex, secondIndex};
    
    class TrieNode
    {
        private Dictionary<char, TrieNode> children;
        public int index;
        public TrieNode()
        {
            children = new Dictionary<char, TrieNode>();
            index = -1;
        }
        
        public bool DoesNotContainsChild(char c) => !children.ContainsKey(c);
        
        public void AddChild(char c) => children[c] = new TrieNode();
        
        public TrieNode GetChild(char c) => children[c];
        
        public int GetIndex() => index;
        
        public void SetIndex(int index) => this.index = index;
        
        public Dictionary<char, TrieNode> GetAllChildren() => children;
    }
    
    class Trie
    {
        TrieNode root;
        public Trie()
        {
            root = new TrieNode();
        }
        
        public void AddWord(string word, int index)
        {
            TrieNode node = root;
            for(int i = word.Length-1 ; i >= 0 ; --i)
            {
                char c = word[i];
                if(node.DoesNotContainsChild(c))
                {
                    node.AddChild(c);
                }
                node = node.GetChild(c);
            }
            node.SetIndex(index);
        }
        
        public TrieNode GetRoot() => root;
    }
    
    class Helper
    {
        List<int> indexes;
        public List<int> GetPalindromeIndexes(TrieNode root, string word)
        {
            indexes = new List<int>();
            ProcessEmptyString(root, word);
            
            TrieNode node = TraverseFullWord(root, word);
            if(node != null)
            {
                FindPalindromeIndexes(node);
            }
            return indexes;
        }
        
        private TrieNode TraverseFullWord(TrieNode node, string word)
        {
            for(int i = 0 ; i < word.Length ; ++i)
            {
                if(i != 0)
                    ProcessRemainingWord(node, word, i);
                char c = word[i];
                if(node.DoesNotContainsChild(c))
                {
                    return null;
                }
                node = node.GetChild(c);
            }
            return node;
        }
        
        private void ProcessEmptyString(TrieNode root, string word)
        {
            CheckIndexAndAdd(root, word);
        }
        
        private void ProcessRemainingWord(TrieNode node, string word, int index)
        {
            CheckIndexAndAdd(node, word, index);
        }
        
        private List<int> FindPalindromeIndexes(TrieNode node)
        {
            List<char> path = new List<char>();
            TraverseRemainingTrie(node, path);
            return indexes;
        }
        
        private void TraverseRemainingTrie(TrieNode node, List<char> path)
        {
            CheckIndexAndAdd(node, path);
            foreach(var child in node.GetAllChildren())
            {
                List<char> newPath = new List<char>(path);
                newPath.Add(child.Key);
                TraverseRemainingTrie(child.Value, newPath);
            }
        }
        
        private void CheckIndexAndAdd(TrieNode node, List<char> path, int start = 0)
        {
            int index = node.GetIndex();
            if(index != -1)
            {
                if(CheckIfPalindrome(path, start))
                {
                    indexes.Add(index);
                }                
            }
        }
        
        private void CheckIndexAndAdd(TrieNode node, string word, int start = 0)
        {
            int index = node.GetIndex();
            if(index != -1)
            {
                if(CheckIfPalindrome(word, start))
                {
                    indexes.Add(index);
                }                
            }
        }
        
        private bool CheckIfPalindrome(List<char> path, int start) 
        {
            int end = path.Count - 1;
            while(start <= end)
            {
                if(path[start] != path[end])
                    return false;
                ++start;
                --end;
            }
            return true;
        }
        
        private bool CheckIfPalindrome(string word, int start) 
        {
            int end = word.Length - 1;
            while(start <= end)
            {
                if(word[start] != word[end])
                    return false;
                ++start;
                --end;
            }
            return true;
        }
    }
}