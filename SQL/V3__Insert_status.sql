USE EstateEmporium;
GO

INSERT INTO [Status] ([StatusID], [Status])
VALUES (0, 'Not started'),
       (1, 'Awaiting home loan'),
       (2, 'Loan approved'),
       (3, 'Payment received'),
       (4, 'Ownership transfer complete'),
	   (5, 'Persona Notified'),
	   (-1, 'Purchase Failed');
GO