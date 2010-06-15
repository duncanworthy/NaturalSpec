﻿// 1. define module
module Spec.CarSellingLib.DealerMockingSample

// 2. open NaturalSpec-Namespace
open NaturalSpec
        
// 3. open project namespace
open CarSellingLib

// define reusable values
let DreamCar = new Car(CarType.BMW, 200)
let LameCar = new Car(CarType.Fiat, 45)

// 4. create a method in BDD-style
let selling_a_car_for amount (dealer:IDealer) =
    printMethod amount
    dealer.SellCar amount

// 5. create a scenario      
[<Scenario>]
let ``When selling the DreamCar for 40000``() =     
    let bert = 
        mock<IDealer> "Bert"
          |> expectCall <@fun x -> x.SellCar @> 40000 (fun price -> DreamCar)

    As bert
      |> When selling_a_car_for 40000
      |> It should equal DreamCar
      |> Verify
    
     
[<Scenario>]
let ``When selling the Lamecar for 19000``() = 
    let bert =
        mock<IDealer> "Bert"
          |> expectCall <@fun x -> x.SellCar @> 19000 (fun price -> LameCar)

    As bert
      |> When selling_a_car_for 19000
      |> It should equal LameCar
      |> Verify

[<Scenario>]
[<FailsWith "Method SellCar was not called with 19000 on Bert">]
let ``When not calling the mocked function``() = 
    let bert =
        mock<IDealer> "Bert"
          |> expectCall <@fun x -> x.SellCar @> 19000 (fun price -> LameCar)

    As bert
      |> Verify

[<Scenario>]
[<FailsWith "Method SellCar was not called with 40000 on Bert">]
let ``When not calling the second mocked function``() = 
    let bert =
        mock<IDealer> "Bert"
          |> expectCall <@fun x -> x.SellCar @> 19000 (fun price -> LameCar)    
          |> expectCall <@fun x -> x.SellCar @> 40000 (fun price -> DreamCar)    

    As bert
      |> When selling_a_car_for 19000
      |> It should equal LameCar
      |> Verify