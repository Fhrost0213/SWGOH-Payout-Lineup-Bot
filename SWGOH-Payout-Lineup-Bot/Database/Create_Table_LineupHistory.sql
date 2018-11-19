CREATE TABLE LineupHistory (
	LineupHistoryId INT NOT NULL AUTO_INCREMENT,
	PlayerId INT NOT NULL,
	LineupHistoryDate DATE,
	OrderNumber INT,
	AddDate DATE,
	AddName VARCHAR(100)
)