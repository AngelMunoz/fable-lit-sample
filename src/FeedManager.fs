[<RequireQualifiedAccess>]
module Pages.FeedManager

open Browser.Types
open Lit
open Fable.Haunted
open Types
open Fable.Core

let private feedTpl feed =
    html
        $"""
         <ion-item>
            <ion-label>{feed.Name}</ion-label>
         </ion-item>
        """

let private feedManager () =
    let currentFeed, setCurrentFeed =
        Haunted.useState ({ Name = ""; Url = "" })

    let feeds, setFeeds = Haunted.useState (Feed.LoadFeeds())

    let nameChanged (ev: CustomEvent<{| value: string |}>) =
        let name =
            ev.detail
            |> Option.map (fun detail -> detail.value)
            |> Option.defaultValue ""

        setCurrentFeed { currentFeed with Name = name }

    let urlChanged (ev: CustomEvent<{| value: string |}>) =
        let url =
            ev.detail
            |> Option.map (fun detail -> detail.value)
            |> Option.defaultValue ""

        setCurrentFeed { currentFeed with Url = url }

    let saveFeed _ =
        if currentFeed.Name.Length > 0
           && currentFeed.Url.Length > 0 then
            let feeds = [| yield! feeds; currentFeed |]
            setFeeds feeds
            Feed.SaveFeeds feeds

    html
        $"""
        <ion-content>
            <h1>Feed List</h1>
            <section>
                <ion-item>
                    <ion-label>Add Feed</ion-label>
                </ion-item>
                <ion-input .debounce={750} placeholder="Name" @ionChange={nameChanged}></ion-input>
                <ion-input .debounce={750} type="url" placeholder="Url" @ionChange={urlChanged}></ion-input>
                <ion-button @click={saveFeed}>Save</ion-button>
            </section>
            <ion-list>
                {feeds |> Array.map feedTpl}
            </ion-list>
        </ion-content>
        """


let register () =
    defineComponent "x-feed-manager" (Haunted.Component feedManager)
