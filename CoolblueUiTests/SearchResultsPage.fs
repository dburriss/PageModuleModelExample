namespace Pages

module SearchResultsPage =
    open Elements
    open canopy.classic
    
    let uri = "https://www.coolblue.nl/zoeken"
    let verifyOn() = on uri
    let searchFor term = Header.searchFor term
    let results() = SearchResults.items()