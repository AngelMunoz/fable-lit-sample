module Types

open Browser.WebStorage
open Fable.Core.JS

type Feed =
    { Name: string
      Url: string }

    static member SaveFeeds(feeds: Feed []) : unit =
        localStorage.setItem ("feeds", JSON.stringify (feeds))

    static member LoadFeeds() : Feed [] =
        localStorage.getItem ("feeds")
        |> Option.ofObj
        |> Option.defaultValue "[]"
        |> JSON.parse
        :?> Feed []

type RssItem =
    { title: string option
      link: string option
      guid: string option
      categories: string array option
      description: string option
      pubDate: string option
      content: string option }

type RssContent =
    { title: string option
      description: string option
      link: string option
      items: RssItem array option }
