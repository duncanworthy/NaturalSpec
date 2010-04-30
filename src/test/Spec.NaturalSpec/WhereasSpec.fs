﻿module NaturalSpec.WhereasSpec

let c = ref 0
let f x =
    printMethod x
    c := 1
    x

[<Scenario>]
let ``Using whereas to switch context``() =  
  Given true
    |> When calculating f
    |> It should equal true
    |> Whereas (!c)
    |> It should equal 1
    |> Verify  