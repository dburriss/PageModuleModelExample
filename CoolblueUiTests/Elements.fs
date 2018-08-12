namespace Elements
open canopy.classic
open Selectors

module Header =
    //selectors
    let searchBox = "#search_query"
    let basketButton = "a[href=\"/winkelmandje\"]"

    //actions
    let searchFor term =
        searchBox << term
        press enter

module SearchResults =
    open OpenQA.Selenium

    type SearchResultElement = {
        ProductId:string
        El:IWebElement
        Name:string
        Price:decimal
        IsAvailable:bool
    }
    let private tee x f = f(x); x
    let private toPrice (s:string) = s.Split(",").[0] |> decimal
    let private getOrderButton itemEl = itemEl |> elementWithin @".product__order-button"
    let private isOrderButton (orderBtnEl:IWebElement) = 
        orderBtnEl 
        |> getAttrValue "class" 
        |> fun s -> s.Split(" ")
        |> Array.contains @"action--order"

    let items () =
        let rowEls = element (sData "component" "products")
                    |> elementsWithin ".card"
        let getId itemEl = itemEl |> elementWithin "a" |> getDataAttrValue "productid"
        let getTitle itemEl = itemEl |> elementWithin "a" |> getAttrValue "title"
        let getPrice itemEl = itemEl |> elementWithin @".product__sales-price" |> read |> toPrice

        rowEls 
        |> List.map (fun itemEl -> 
                                let id = itemEl |> getId
                                {
                                    ProductId = id
                                    El = itemEl
                                    Name = itemEl |> getTitle
                                    Price = itemEl |> getPrice
                                    IsAvailable = itemEl |> getOrderButton |> isOrderButton
                                })    