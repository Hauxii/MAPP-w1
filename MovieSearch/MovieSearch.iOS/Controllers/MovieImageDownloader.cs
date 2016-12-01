using System;
using MovieSearch.MovieDownload;
using MovieSearch.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;

namespace MovieSearch.iOS
{
	public class MovieImageDownloader
	{
		private ImageDownloader _imageDl;

		public MovieImageDownloader()
		{
			this._imageDl = new ImageDownloader(new StorageClient());
		}

		public async Task GetMoviesByTitle(Movies movies, string title)
		{
			MovieDbFactory.RegisterSettings(new DBSettings());
			var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

			var movieInfoResponse = await movieApi.SearchByTitleAsync(title);

			movies.ClearList();

			foreach (var m in movieInfoResponse.Results)
			{
				ApiQueryResponse<MovieCredit> movieCreditsResponse = await movieApi.GetCreditsAsync(m.Id);

				var localFilePath = _imageDl.LocalPathForFilename(m.PosterPath);
				if (localFilePath != string.Empty)
				{
					var image = _imageDl.DownloadImage(m.PosterPath, localFilePath, CancellationToken.None);
				}
				m.PosterPath = localFilePath;

				movies.ExtractInfo(m, movieCreditsResponse);

			}
			return;
		}

	}
}
