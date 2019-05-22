# TimeTell
A component for LiveSplit ( https://livesplit.org/ )  

## What it is:
TL;DR: It shows what time you should aim for when trying to PB  
When trying to set a new personal best, you probably often reset/retry in a middle of a run when you FEEL it's better to retry than to continue on a bad run.  
This component will try to calculate when it's ACTUALLY worth to reset, based on your split history.

## Installation:
1. Place TimeTell.dll into the Components directory of your LiveSplit installation.
2. Open LiveSplit. Right click -> Edit Layout -> [Giant "+" Button] -> Information -> TimeTell
3. Go to Layout Settings and click on the TimeTell tab.  
  3.1. You can configure how many of your most recent attempts will be used in calculations, uou can either have it use a percentage of your most recent attempts, or just a fixed number of your most recent attempts.  
  3.2. The other available options here are related to the quality/accuracy of the results, details below.  
  3.3. Press "COMPUTE" and wait until finished. This step is never automated, and must be manually repeated for updated results.  
  
## Pictures:
![Method1](/images/timetellExample2.jpg)

# Detailed explanation of what it does:
## Estimate PB chance:
First it estimates the chance of setting a new PB after a reset, by using combinations of your history of most recent splits.  
These combinations together are used as possible run outcomes.  
PB chance = (number of outcomes that beats your pb) / (total number of outcomes)  
If the run category has many segments/parts, and you have a large number of attempts for each of these segments, the number of combinations quickly "blows up". Basically the formula is (number of attempts)^(number of segments). For example, a history of 2000 attempts in a category with 20 segment parts, that is 2000^20, which comes out as "1.048576e+66", a number way too large to do any computations with.  
To deal with this, we need to compress the number combinations into fewer(how many are decided by "Max precision" in settings), there are two available methods:  
1. This is the default method. Average neighbouring split times together.
![Method1](/images/method1.png)
2. This is the "Alternative computation mode". Discard every X split, it never discard the best split. This guarantees that one of the combinations, is the same as "sum of best segments" (for the most recent splits used).
![Method1](/images/method2.png)

## Compute what estimates a good/bad time for each segment:
To estimate a PB chance from a segment other than the first one, we need to know the "pace" the run is on(time played in an active run).  
What constitutes a GOOD "pace" for each segment is what TimeTell is trying to calculate, and it's defined by the chance to PB _AND_ what duration of the run is left before it ends.  
The logic goes like this:  
  If a new run has X% chance of beating PB, then an active run, half-way completed, that has (X/2) % chance of beating PB is equally good.  
For each segment, effectively all possible "paces" are tested, until one is found that is equally time-efficient as an attempt from start is.  
