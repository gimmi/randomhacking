package com.github.gimmi.spikescalaunfilteredmvn

import unfiltered.request.{GET, Seg, Path}
import unfiltered.response.ResponseString
import unfiltered.filter.request.ContextPath

class Echo extends unfiltered.filter.Plan {
  def intent = {
    case GET(ContextPath(_, Seg("items" :: Nil))) => ResponseString("Getting items")
    case GET(ContextPath(_, Seg("items" :: id :: Nil))) => ResponseString("Getting item #" + id)
    case GET(ContextPath(_, Seg("items" :: id :: "comments" :: Nil))) => ResponseString("Getting comments for item #" + id)
    case GET(ContextPath(_, Seg("items" :: itemId :: "comments" :: commentId :: Nil))) => ResponseString(s"Getting comment #$commentId for item #$itemId")
    case ContextPath(_, path) => ResponseString(s"No action defined for path $path")
  }
}
