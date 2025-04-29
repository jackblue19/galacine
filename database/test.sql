USE CinemaSystem;
GO

UPDATE Movie
SET MovieImg = 'https://i.ebayimg.com/images/g/zRAAAOSwvaJgJSg1/s-l1600.webp'
WHERE MovieStatus = 'Active';


SELECT * FROM [Movie]
SELECT * FROM [MovieDetail]
SELECT * FROM [MovieGenre]
SELECT * FROM [MovieBasePrice]

-- fix movie status in the index view

-- ticking or not for IsSub

-- choose releaseDate

-- choose AgeLimit as well as  Language from Move and MovieDetail must be the same 