SELECT a.first_name AS AuthorName,
	   a.middle_name AS AuthorMiddleName,
	   a.last_name AS AuthorLastName,
	   b.title AS BookTittle,
	   p.[name] AS PublisherName,
	   g.genre AS Genre
FROM books AS b
LEFT JOIN book_authors AS ba ON b.book_id = ba.book_id
LEFT JOIN authors AS a ON a.author_id = ba.author_id
LEFT JOIN publishers AS p ON p.publsher_id = b.publisher_id
LEFT JOIN book_genres AS bg ON bg.book_id = b.book_id
LEFT JOIN genres AS g ON bg.genre_id = g.genre_id
LEFT JOIN genres AS parent ON g.genre_id <> parent.genre_id
ORDER BY g.genre