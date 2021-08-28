[<RequireQualifiedAccess>]
module Pages.Home

open Fable.Core

open Lit
open Haunted
open Types
open Browser.Types


let private home () =
    let feeds, setFeeds = Haunted.useState (Feed.LoadFeeds())
    let selectedFeed, setSelectedFeed = Haunted.useState<Feed option> None

    let feedItemTpl (feed: Feed) =
        html
            $"""
            <ion-item button @click={fun _ -> setSelectedFeed (Some feed)}>
                <ion-label>{feed.Name}</ion-label>
            </ion-item>
        """

    let feedTpl =
        if feeds.Length > 0 then
            html
                $"""
                <ion-list>
                    <ion-list-header>
                        <ion-label>My Feeds. <small>(pull to refresh)</small></ion-label>
                    </ion-list-header>
                    {feeds |> Array.map feedItemTpl}
                </ion-list>
            """
        else
            html
                $"""
                <h4>Looks like you don't have any feeds</h4>
                <p>Try adding one on the Feeds tab</p>
            """

    let refreshContent (ev: CustomEvent<{| complete: unit -> JS.Promise<unit> |}>) =
        let feeds = Feed.LoadFeeds()

        ev.detail
        |> Option.map (fun detail -> detail.complete () |> ignore)
        |> ignore

        setFeeds feeds


    let unSelectedTpl =
        html
            $"""
            <ion-refresher slot="fixed" @ionRefresh={refreshContent}>
                <ion-refresher-content></ion-refresher-content>
            </ion-refresher>
            <h1>Welcome back</h1>
            {feedTpl}
            """

    let selectedTpl feed =
        html $"""<x-feed-viewer .feed={feed}></x-feed-viewer>"""

    let content =
        match selectedFeed with
        | Some feed -> selectedTpl feed
        | None -> unSelectedTpl

    html
        $"""
        <ion-content @x-go-back={fun _ -> setSelectedFeed None}>
            {content}
        </ion-content>
        """

let register () =
    defineComponent "x-home" (Haunted.Component home)
