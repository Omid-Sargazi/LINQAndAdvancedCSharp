-- SELECT COUNT(*)  FROM dbo.Book as b JOIN dbo.BookCopy as bc on b.Id = bc.BookId where Status = 'Available'


-- SELECT me.FullName,me.Email,me.Phone,b.Title,br.DueDate from Borrow as br join Member as me on br.MemberId = me.Id JOIN BookCopy as bc on bc.Id = br.BookCopyId JOIN Book as b on b.Id = bc.BookId
-- WHERE br.ReturnDate IS NULL and br.DueDate<GetDate()

SELECT me.Id,me.FullName from FineTransaction as ft JOIN Member as me on ft.MemberId = me.Id where(ft.FineAmount-ft.PaidAmount)>0 GROUP BY me.Id,me.FullName HAVING SUM(ft.FineAmount- ft.PaidAmount)>0