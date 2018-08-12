module Selectors
    open System
    open OpenQA.Selenium
    let sData prop value = sprintf "[data-%s=\"%s\"]" prop value
    let getDataAttrValue prop (el:IWebElement) = el.GetAttribute(sprintf "data-%s" prop)
    let getAttrValue prop (el:IWebElement) = el.GetAttribute(sprintf "%s" prop)
    let getSomeAttrValue prop (el:IWebElement) = match el.GetAttribute(sprintf "%s" prop) with
                                                    | x when String.IsNullOrEmpty(x) -> None
                                                    | x -> Some x