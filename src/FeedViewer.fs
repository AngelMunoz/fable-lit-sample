[<RequireQualifiedAccess>]
module Pages.FeedViewer

open Browser.Types
open Fable.Core
open Fable.Core.JsInterop

open Lit
open Haunted

open Types

let private getRssContent (url: string) : JS.Promise<Result<RssContent, string>> = importMember "./interop.js"

let private goBack (event: Event) =
    let evt =
        Haunted.createEvent (
            "x-go-back",
            {| bubbles = true
               composed = true
               cancelable = true |}
        )

    dispatchEvent evt event.target

let private renderItem (item: RssItem) =
    html
        $"""
        <ion-item href={ifSome item.link} target="_blank" rel="noreferer noopener">
            <ion-label>{item.title}</ion-label>
            <ion-note slot="end">{item.pubDate}</ion-note>
        </ion-item>
        """

let private rssContentTpl items =
    items
    |> Option.defaultValue Array.empty
    |> Array.map renderItem

let private view (props: {| feed: Feed option |}) =
    let feed =
        defaultArg props.feed { Name = ""; Url = "" }

    let err, setErr = Haunted.useState<string option> None

    let content, setContent =
        Haunted.useState<RssContent option> None

    Haunted.useEffect (
        (fun _ ->
            (getRssContent(feed.Url)
                .``then`` (fun res ->
                    match res with
                    | Ok value -> Some value |> setContent
                    | Error err -> Some err |> setErr)
             |> ignore)),
        [||]
    )

    let renderFeed (content: RssContent option) =
        match content with
        | Some content ->
            html
                $"""
                <article>
                    <header>
                        <h1>{content.title}</h1>
                        <ion-button fill="clear" href={content.link} target="_blank" rel="noreferer noopener">{content.link}</ion-button>
                    </header>
                    <ion-list>
                        {rssContentTpl content.items}
                    </ion-list>
                </article>
                """
        | None -> html $"""<p>The feed "{feed.Url}" seems empty</p> """

    html
        $"""
        <ion-toolbar>
            <ion-title>{feed.Name}</ion-title>
            <ion-buttons slot="secondary">
                <ion-button fill="solid" @click={goBack}>
                    <ion-icon name="arrow-back-outline"></ion-icon>
                    Back
                </ion-button>
            </ion-buttons>
        </ion-toolbar>
        {err}
        {renderFeed content}
        """

let register () =
    defineComponent "x-feed-viewer" (Haunted.Component view)
