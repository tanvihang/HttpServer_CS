﻿namespace HttpServerBasic;

public class RouteAttribute: Attribute
{
    public string Path { get; }

    public RouteAttribute(string path)
    {
        Path = path;
    }
}