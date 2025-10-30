-- SELECT COUNT(*)  FROM dbo.Book as b JOIN dbo.BookCopy as bc on b.Id = bc.BookId where Status = 'Available'


-- SELECT me.FullName,me.Email,me.Phone,b.Title,br.DueDate from Borrow as br join Member as me on br.MemberId = me.Id JOIN BookCopy as bc on bc.Id = br.BookCopyId JOIN Book as b on b.Id = bc.BookId
-- WHERE br.ReturnDate IS NULL and br.DueDate<GetDate()

-- SELECT me.Id,me.FullName from FineTransaction as ft JOIN Member as me on ft.MemberId = me.Id where(ft.FineAmount-ft.PaidAmount)>0 GROUP BY me.Id,me.FullName HAVING SUM(ft.FineAmount- ft.PaidAmount)>0


-- SELECT m.FullName,b.Title, br.DueDate,br.BorrowDate,bc.CopyNumber from Borrow br join Member m on br.MemberId = m.Id join BookCopy as bc on br.BookCopyId = bc.Id join Book  as b on b.Id = bc.BookId

-- SELECT b.Title,b.Author,bc.CopyNumber,bc.Status,br.BorrowDate,br.DueDate,m.FullName from Book as b JOIN BookCopy as bc on b.Id = bc.BookId JOIN Borrow as br on br.BookCopyId = bc.Id LEFT JOIN Member as m on m.Id = br.Me


-- INSERT INTo Category(Name,ParentId) VALUES
-- ('Classical Mechanics', 2),
-- ('Quantum Physics', 2),
-- ('Thermodynamics', 2);

-- INSERT INTO Category (Name, ParentId) VALUES 
-- ('Organic Chemistry', 3),
-- ('Inorganic Chemistry', 3),
-- ('Analytical Chemistry', 3);
-- -- اضافه کردن زیردسته‌های بیشتر برای تاریخ
-- INSERT INTO Category (Name, ParentId) VALUES 
-- ('Ancient History', 4),
-- ('Medieval History', 4),
-- ('Modern History', 4);
-- INSERT INTO Category (Name, ParentId) VALUES 
-- ('Persian Literature', 5),
-- ('English Literature', 5),
-- ('French Literature', 5);

-- INSERT INTO Category (Name, ParentId) VALUES 
-- ('Newtonian Mechanics', 6),  -- فرزند Classical Mechanics
-- ('Relativity', 6),           -- فرزند Classical Mechanics
-- ('Quantum Computing', 7),    -- فرزند Quantum Physics
-- ('Nuclear Physics', 7),      -- فرزند Quantum Physics
-- ('Biochemistry', 9),         -- فرزند Organic Chemistry
-- ('Pharmaceutical Chemistry', 9), -- فرزند Organic Chemistry
-- ('Roman Empire', 12),        -- فرزند Ancient History
-- ('Greek History', 12),       -- فرزند Ancient History
-- ('Poetry', 15),              -- فرزند Persian Literature
-- ('Fiction', 15);
SELECT c1.Name as Level1,c2.Name as Level2 , c3.Name as Level3 from Category c1 LEFT JOIN Category as c2 on c1.Id = c2.ParentId LEFT JOIN Category c3 on c2.Id = c3.ParentId  WHERE c1.ParentId is Null ORDER BY
c1.Name,c2.Name , c3.Name