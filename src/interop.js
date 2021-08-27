import { FSharpResult$2 } from './.fable/fable-library.3.2.12/Choice.js';
import { ofNullable } from './.fable/fable-library.3.2.12/Option.js';
import { RssContent, RssItem } from './Types.fs.js';

/**
 * @param {XMLDocument} xml
 */
function parseRssItem(xml) {
    const title = xml.querySelector('title')?.textContent;
    const link = xml.querySelector('link')?.textContent;
    const guid = xml.querySelector('guid')?.textContent;
    const preCategories = xml.querySelectorAll('categories');
    const description = xml.querySelector('description')?.textContent;
    const pubDate = xml.querySelector('pubDate')?.textContent;
    const content = xml.querySelector('content')?.textContent;
    const categories = Array.from(preCategories).map(cat => cat?.textContent);
    return new RssItem(ofNullable(title), ofNullable(link), ofNullable(guid), ofNullable(categories), ofNullable(description), ofNullable(pubDate), ofNullable(content));
}

/**
 * @param {XMLDocument} xml
 */
function parseRssContent(xml) {
    const channel = xml.querySelector('channel');
    const title = channel.querySelector('title')?.textContent;
    const description = channel.querySelector('description')?.textContent;
    const link = channel.querySelector('link:not([rel=self])')?.textContent;
    const preItems = channel.querySelectorAll('item');
    const items = Array.from(preItems).map(item => parseRssItem(item));
    return new RssContent(ofNullable(title), ofNullable(description), ofNullable(link), ofNullable(items));
}

export async function getRssContent(url) {
    if (typeof url !== 'string') return new FSharpResult$2(1, "url must be a string");

    const response = await fetch(url);

    if (!response.ok) return new FSharpResult$2(1, `${response.status} - ${response.statusText}`);

    try {
        const text = await response.text();
        const parser = new DOMParser();
        const feed = parseRssContent(parser.parseFromString(text, 'text/xml'));
        return new FSharpResult$2(0, feed);
    } catch (error) {
        return new FSharpResult$2(1, error.message);
    }
}