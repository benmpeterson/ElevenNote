-- 22918af0-6b95-429f-9acd-1813ebb235e6

--* means everything
select NoteId, Title, IsStarred, CreatedUtc 
from dbo.Note
where Owner = '22918af0-6b95-429f-9acd-1813ebb235e6'


update Note
set Title = 'My Third note !!!!!', ModifiedUTC = GETUTCDATE()
where NoteId = 3

--Database server is maintaining note ID so you don't have to add it
delete from Note
where NoteId = 3

--
