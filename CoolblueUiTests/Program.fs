open canopy.runner.classic
open canopy.configuration
open canopy.classic
open Elements
open Pages
open Swensen.Unquote

chromeDir <- "C:\\tools\\selenium\\"
//start an instance of chrome
start chrome

before (fun _ -> HomePage.goto)

context "Smoke tests"
skipAllTestsOnFailure <- true
"home page loads" &&& fun _ -> displayed HomePage.homePageBanner
"search box available" &&& fun _ -> displayed Header.searchBox
"cart is available" &&& fun _ -> displayed Header.basketButton
"No products on search page are free" &&& fun _ -> 
    HomePage.searchFor ""
    let results = SearchResultsPage.results()
    test <@ results |> List.forall (fun x -> x.Price > 0m) @>
    // last smake test. Disable skip.
    skipAllTestsOnFailure <- false

context "Search tests"

"Search for laptops has results" &&& fun _ -> 
    HomePage.searchFor "Laptops"
    let results = SearchResultsPage.results()
    test <@ not(List.isEmpty results) @>

"First 3 laptop results are available" &&& fun _ -> 
    HomePage.searchFor "Laptops"
    let results = SearchResultsPage.results() |> List.take 3
    test <@ results |> List.forall (fun x -> x.IsAvailable) @>

"No laptops are free" &&& fun _ -> 
    HomePage.searchFor "Laptops"
    let results = SearchResultsPage.results()
    test <@ results |> List.forall (fun x -> x.Price > 0m) @>

run()

printfn "press [enter] to exit"
System.Console.ReadLine() |> ignore

quit()