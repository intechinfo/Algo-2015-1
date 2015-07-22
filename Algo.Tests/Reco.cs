﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace Algo.Tests
{
    [TestFixture]
    public class Reco
    {
        static string _badDataPath = @"E:\Intech\2015-1\S9-10\Algo\ThirdParty\MovieData\MovieLens\";
        static string _goodDataPath = @"E:\Intech\2015-1\S9-10\Algo\ThirdParty\MovieData\";

        [Test]
        public void CorrectData()
        {
            Dictionary<int, Movie> firstMovies;
            Dictionary<int, List<Movie>> duplicateMovies;
            Movie.ReadMovies( Path.Combine( _badDataPath, "movies.dat" ), out firstMovies, out duplicateMovies );
            int idMovieMin = firstMovies.Keys.Min();
            int idMovieMax = firstMovies.Keys.Max();
            Console.WriteLine( "{3} Movies from {0} to {1}, {2} duplicates.", idMovieMin, idMovieMax, duplicateMovies.Count, firstMovies.Count ); 

            Dictionary<int, User> firstUsers;
            Dictionary<int, List<User>> duplicateUsers;
            User.ReadUsers( Path.Combine( _badDataPath, "users.dat" ), out firstUsers, out duplicateUsers );
            int idUserMin = firstUsers.Keys.Min();
            int idUserMax = firstUsers.Keys.Max();
            Console.WriteLine( "{3} Users from {0} to {1}, {2} duplicates.", idUserMin, idUserMax, duplicateUsers.Count, firstUsers.Count );

            Dictionary<int,string> badLines;
            int nbRating = User.ReadRatings( Path.Combine( _badDataPath, "ratings.dat" ), firstUsers, firstMovies, out badLines );
            Console.WriteLine( "{0} Ratings: {1} bad lines.", nbRating, badLines.Count );

            Directory.CreateDirectory( _goodDataPath );
            // Saves Movies
            using( TextWriter w = File.CreateText( Path.Combine( _goodDataPath, "movies.dat" ) ) )
            {
                int idMovie = 0;
                foreach( Movie m in firstMovies.Values )
                {
                    m.MovieID = ++idMovie;
                    w.WriteLine( "{0}::{1}::{2}", m.MovieID, m.Title, String.Join( "|", m.Categories ) );
                }
            }

            // Saves Users
            string[] occupations = new string[]{
                "other", 
                "academic/educator", 
                "artist", 
                "clerical/admin",
                "college/grad student",
                "customer service",
                "doctor/health care",
                "executive/managerial",
                "farmer",
                "homemaker",
                "K-12 student",
                "lawyer",
                "programmer",
                "retired",
                "sales/marketing",
                "scientist",
                "self-employed",
                "technician/engineer",
                "tradesman/craftsman",
                "unemployed",
                "writer" };
            using( TextWriter w = File.CreateText( Path.Combine( _goodDataPath, "users.dat" ) ) )
            {
                int idUser = 0;
                foreach( User u in firstUsers.Values )
                {
                    u.UserID = ++idUser;
                    string occupation;
                    int idOccupation;
                    if( int.TryParse( u.Occupation, out idOccupation ) 
                        && idOccupation >= 0 
                        && idOccupation < occupations.Length )
                    {
                        occupation = occupations[idOccupation];
                    }
                    else occupation = occupations[0];
                    w.WriteLine( "{0}::{1}::{2}::{3}::{4}", u.UserID, u.Male ? 'M' : 'F', u.Age, occupation, "US-"+u.ZipCode );
                }
            }
            // Saves Rating
            using( TextWriter w = File.CreateText( Path.Combine( _goodDataPath, "ratings.dat" ) ) )
            {
                foreach( User u in firstUsers.Values )
                {
                    foreach( var r in u.Ratings )
                    {
                        w.WriteLine( "{0}::{1}::{2}", u.UserID, r.Key.MovieID, r.Value );
                    }
                }
            }
        }

        [Test]
        public void ReadMovieData()
        {
            RecoContext c = new RecoContext();
            c.LoadFrom( _goodDataPath );
            for( int i = 0; i < c.Users.Length; ++i )
                Assert.That( c.Users[i].UserID, Is.EqualTo( i+1 ) );
            for( int i = 0; i < c.Movies.Length; ++i )
                Assert.That( c.Movies[i].MovieID, Is.EqualTo( i+1 ) );
        }

        [Test]
        public void computing_the_distance_between_2_users()
        {
            RecoContext c = new RecoContext();
            c.LoadFrom( _goodDataPath );

            const int part = 10;

            for( int i = 0; i < c.Users.Length/part; i++ )
            {
                var u1 = c.Users[i];
                Assert.That( c.DistanceNorm2( u1, u1 ), Is.EqualTo( 0 ) );
                for( int j = i + 1; j < c.Users.Length/part; j++ )
                {
                    var u2 = c.Users[j];
                    Assert.That( c.DistanceNorm2( u1, u2 ), Is.EqualTo( c.DistanceNorm2( u2, u1 ) ) );
                }
            }
        }


        [Test]
        public void computing_recommended_movies()
        {
            RecoContext c = new RecoContext();
            c.LoadFrom( _goodDataPath );

            User u = c.Users[3712];
            //List<Movie> recoMovies = c.GetRecoMovies( u, 15 );
            //SimilarUser[] similarUsers = c.GetSimilarUsers( u, 200 );
        }

        [Test]
        public void test_BestKeeper()
        {
            BestKeeper<int> b = new BestKeeper<int>( 5, (x,y) => y - x );
            Assert.That( b.Count, Is.EqualTo( 0 ) );
            Assert.That( b.Add( 5 ) );
            Assert.That( b.Add( 3 ) );
            Assert.That( b.Add( 4 ) );
            CollectionAssert.AreEqual( new int[] { 5, 4, 3 }, b );
            Assert.That( b.Add( 2 ) );
            Assert.That( b.Add( 1 ) );
            Assert.That( b.Add( 0 ), Is.False );
            CollectionAssert.AreEqual( new int[] { 5, 4, 3, 2, 1 }, b );
            Assert.That( b.Add( 1000 ) );
            CollectionAssert.AreEqual( new int[] { 1000, 5, 4, 3, 2 }, b );
            Assert.That( b.Add( 2000 ) );
            Assert.That( b.Add( 3000 ) );
            Assert.That( b.Add( 4000 ) );
            Assert.That( b.Add( 5000 ) );
            Assert.That( b.Add( 1 ), Is.False );
            CollectionAssert.AreEqual( new int[] { 5000, 4000, 3000, 2000, 1000 }, b );
        }

    }
}
