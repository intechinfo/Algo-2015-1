using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Algo
{
    public class RecoContext
    {
        public User[] Users { get; private set; }
        public Movie[] Movies { get; private set; }

        public void LoadFrom( string folder )
        {
            Users = User.ReadUsers( Path.Combine( folder, "users.dat" ) );
            Movies = Movie.ReadMovies( Path.Combine( folder, "movies.dat" ) );
            User.ReadRatings( Users, Movies, Path.Combine( folder, "ratings.dat" ) );
        }

        public double DistanceNorm2( User u1, User u2 )
        {
            if( u1 == u2 && u1.Ratings.Count == 0 ) return 0.0;
            bool atLeastOneMovieInCommon = false;
            double sumSquare = 0;
            foreach( var r in u1.Ratings )
            {
                // U1 has rated movieU1 with ratingU1.
                Movie movieU1 = r.Key;
                int ratingU1 = r.Value;
                // Does U2 have an advice about this movie?
                int ratingU2;
                if( u2.Ratings.TryGetValue( movieU1, out ratingU2 ) )
                {
                    // Yes, U2 have seen it.
                    atLeastOneMovieInCommon = true;
                    // We sum the square of the difference.
                    sumSquare += Math.Pow( ratingU1 - ratingU2, 2 );
                }
            }
            return atLeastOneMovieInCommon ? Math.Sqrt( sumSquare ) : Double.PositiveInfinity;
        }

        public double Similarity( User u1, User u2 )
        {
            return 1 / (1 + DistanceNorm2( u1, u2 ));
        }
    
    }
}
