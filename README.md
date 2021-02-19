# open-anagrams
an open source anagrams project

This is a project that I started since my girlfriend couldn't get enough of the anagrams on Messages. I programmed this for fun, so that she could "play vs herself" instead of spamming me with games. 

It uses one large word list (sourced from the Collins Scrabble Word List), and builds a tree of letters, which are "accepting" if the node is the last letter in a word.

This sort of data structure is incredibly fast at searching up words and could be useful for things like autocorrect or spell checking, provided a good dictionary is loaded into it. The LightWordTree is much more efficient, granted that a lot of storage is saved (no hashmap) and the runtime is generally fast since words aren't usually very long anyways.

# development progress

I haven't worked on this in a while, but plan on revisiting it to give it a proper GUI implementation. Fortunately, the Anagrams library within it is enough to generate text files of all the word sets - making it possible to simply load a single word set for each game (as opposed to generating it from the tree at runtime).
