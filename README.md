# LuwareListImplementation

Notes

Tests: 
I followed an "outside-in" approach, whereas I look for the observable behavior, trying not to reveal the implementation. Thats why there's no particular test checking for how
internal state is sliced into multiple arrays etc. 

Also, I could've added scenarios with different types other than int, but figured I'd be testing the framework more than anything.

Implementation:
  Error handling - I took the liberty to practically ignore any error handling or safety code, there's a dozen places I could check if indexes were inside the bounds of the array. 
  That'd have increased the number of lines considerable. Also the amount of tests. If this is something you see as a negative point I can look into it.
  
  Performance - I'm aware that the addition is going to be problematic incase someone adds 10k records at once. I'm not buffering the array size, I'm creating a new one with +1 size 
  everytime. 
  
  Use of Span - I've found out about Span just as I was looking for a way to slice an array without using copy (which is the standard way). I hope that doesn't count as violating the rules :)
  
  Version - My initial idea was to hash the internal array. I tried a number of ways to do it but the code was getting more complicated than I wanted, a simple counter at every writing
  change seemed enough. I'm a bit skeptical about thread safety but I'm  at least incremeting it always, that reduces the chances of collision. 
