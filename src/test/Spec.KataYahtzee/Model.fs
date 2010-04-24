﻿module Yahtzee.Model

type Roll = int * int * int * int * int

type Category =
| Ones
| Twos
| Threes
| Fours
| Fives
| Sixes
| Pair
| TwoPair
| ThreeOfAKind
| FourOfAKind
| SmallStraight

let toList (roll:Roll) =
    let a,b,c,d,e = roll
    [a;b;c;d;e]

let sumNumber number =
    Seq.filter ((=) number)
      >> Seq.sum

let sumAsTuple value list number =
    let numberCount = list |> Seq.filter ((=) number) |> Seq.length
    if numberCount >= value then value * number else 0
    
let allNumbers = [1..6]
let allPairs =
    [for i in allNumbers do
       for j in allNumbers -> i,j]
      
let takeBest = Seq.max

let takeBestTuple value list =
    allNumbers
        |> Seq.map (sumAsTuple value list)
        |> takeBest

let calcValue category roll =
    let list = toList roll
    match category with
    | Ones   -> sumNumber 1 list
    | Twos   -> sumNumber 2 list
    | Threes -> sumNumber 3 list
    | Fours  -> sumNumber 4 list
    | Fives  -> sumNumber 5 list
    | Sixes  -> sumNumber 6 list
    | Pair   -> takeBestTuple 2 list
    | TwoPair   -> 
        allPairs
          |> Seq.filter (fun (a,b) -> a <> b)
          |> Seq.map (fun (a,b) -> 
                let a' = sumAsTuple 2 list a
                let b' = sumAsTuple 2 list b
                if a' = 0 || b' = 0 then 0 else a' + b')
          |> takeBest
    | ThreeOfAKind -> takeBestTuple 3 list
    | FourOfAKind  -> takeBestTuple 4 list
    | SmallStraight -> 
        match list |> List.sort with
        | [1;2;3;4;5] -> 15
        | _ -> 0