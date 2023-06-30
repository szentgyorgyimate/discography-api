using WebAPI.Common.Helpers;
using WebAPI.Common.Responses.Album;
using WebAPI.Entities;

namespace WebAPI.Common.Mappers;

public static class AlbumMappers
{
    public static AlbumData ToAlbumData(this Album album) =>
        new()
        {
            Id = album.Id,
            Name = album.Name,
            TypeId = album.AlbumTypeId,
            Type = album.AlbumType.Name,
            BandId = album.BandId,
            BandName = album.Band.Name,
            ReleaseDate = album.ReleaseDate.ToShortJsonDateString()
        };
}
