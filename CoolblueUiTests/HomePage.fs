namespace Pages

module HomePage =
    open canopy.classic
    open Elements

    let uri = "https://www.coolblue.nl/"

    let goto = url uri

    let homePageBanner = ".front-page-leaderboard"

    let searchFor term = Header.searchFor term