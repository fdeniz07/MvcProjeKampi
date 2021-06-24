/*
SELECT listelenmesini istediğimiz kolonlar FROM tablo1Adı
INNER JOIN tablo2Adı ON tablo1.kolon1 = tablo2.kolon2
INNER JOIN tablo3Adı ON tablo2.kolon2 = tablo3.kolon3
*/


/*Kullanicilarin Kategori Adina ve Icerik Tarihine göre,
hangi kategoride hangi basliga hangi icerigi yazdigini görüntüleme
*/

SELECT WriterMail,CategoryName,HeadingName,ContentValue,ContentDate
FROM Writers AS wr
INNER JOIN Headings AS hd
ON wr.WriterId=hd.WriterId
INNER JOIN Contents AS con
ON con.HeadingId = hd.HeadingId
INNER JOIN Categories AS cat
ON cat.CategoryId=hd.CategoryId
ORDER BY cat.CategoryName,con.ContentDate