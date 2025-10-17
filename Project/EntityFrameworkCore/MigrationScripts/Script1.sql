CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Coach" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Coach" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "CreatedDate" TEXT NOT NULL
);

CREATE TABLE "Team" (
    "TeamId" INTEGER NOT NULL CONSTRAINT "PK_Team" PRIMARY KEY AUTOINCREMENT,
    "CreatedDate" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250829100514_InitialMigration', '9.0.8');

ALTER TABLE "Team" ADD "Name" TEXT NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250829125053_Migration2', '9.0.8');

INSERT INTO "Team" ("TeamId", "CreatedDate", "Name")
VALUES (1, '2023-01-01 00:00:00', 'Tivoli Gardens FC');
SELECT changes();

INSERT INTO "Team" ("TeamId", "CreatedDate", "Name")
VALUES (2, '2023-01-01 00:00:00', 'Waterhouse FC');
SELECT changes();

INSERT INTO "Team" ("TeamId", "CreatedDate", "Name")
VALUES (3, '2023-01-01 00:00:00', 'Humble Lions FC');
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250829131757_SeededTeams', '9.0.8');

CREATE TABLE "ef_temp_Team" (
    "TeamId" INTEGER NOT NULL CONSTRAINT "PK_Team" PRIMARY KEY AUTOINCREMENT,
    "CreatedDate" TEXT NOT NULL,
    "Name" TEXT NULL
);

INSERT INTO "ef_temp_Team" ("TeamId", "CreatedDate", "Name")
SELECT "TeamId", "CreatedDate", "Name"
FROM "Team";

CREATE TABLE "ef_temp_Coach" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Coach" PRIMARY KEY AUTOINCREMENT,
    "CreatedDate" TEXT NOT NULL,
    "Name" TEXT NULL
);

INSERT INTO "ef_temp_Coach" ("Id", "CreatedDate", "Name")
SELECT "Id", "CreatedDate", "Name"
FROM "Coach";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;
DROP TABLE "Team";

ALTER TABLE "ef_temp_Team" RENAME TO "Team";

DROP TABLE "Coach";

ALTER TABLE "ef_temp_Coach" RENAME TO "Coach";

COMMIT;

PRAGMA foreign_keys = 1;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250916072737_StringsSetToNullable', '9.0.8');

BEGIN TRANSACTION;
ALTER TABLE "Team" RENAME COLUMN "TeamId" TO "Id";

ALTER TABLE "Team" ADD "CoachId" INTEGER NOT NULL DEFAULT 0;

ALTER TABLE "Team" ADD "CreatedBy" TEXT NULL;

ALTER TABLE "Team" ADD "LeagueId" INTEGER NOT NULL DEFAULT 0;

ALTER TABLE "Team" ADD "ModifiedBy" TEXT NULL;

ALTER TABLE "Team" ADD "ModifiedDate" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE "Coach" ADD "CreatedBy" TEXT NULL;

ALTER TABLE "Coach" ADD "ModifiedBy" TEXT NULL;

ALTER TABLE "Coach" ADD "ModifiedDate" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

CREATE TABLE "League" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_League" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL,
    "CreatedDate" TEXT NOT NULL,
    "ModifiedDate" TEXT NOT NULL,
    "CreatedBy" TEXT NULL,
    "ModifiedBy" TEXT NULL
);

CREATE TABLE "Match" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Match" PRIMARY KEY AUTOINCREMENT,
    "HomeTeamId" INTEGER NOT NULL,
    "AwayTeamId" INTEGER NOT NULL,
    "TicketPrice" TEXT NOT NULL,
    "Date" TEXT NOT NULL,
    "CreatedDate" TEXT NOT NULL,
    "ModifiedDate" TEXT NOT NULL,
    "CreatedBy" TEXT NULL,
    "ModifiedBy" TEXT NULL
);

UPDATE "Team" SET "CoachId" = 0, "CreatedBy" = NULL, "LeagueId" = 0, "ModifiedBy" = NULL, "ModifiedDate" = '0001-01-01 00:00:00'
WHERE "Id" = 1;
SELECT changes();


UPDATE "Team" SET "CoachId" = 0, "CreatedBy" = NULL, "LeagueId" = 0, "ModifiedBy" = NULL, "ModifiedDate" = '0001-01-01 00:00:00'
WHERE "Id" = 2;
SELECT changes();


UPDATE "Team" SET "CoachId" = 0, "CreatedBy" = NULL, "LeagueId" = 0, "ModifiedBy" = NULL, "ModifiedDate" = '0001-01-01 00:00:00'
WHERE "Id" = 3;
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250919093440_AddedMoreEntities', '9.0.8');

INSERT INTO "League" ("Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name")
VALUES (1, NULL, '0001-01-01 00:00:00', NULL, '0001-01-01 00:00:00', 'Jamaica Premiere League');
SELECT changes();

INSERT INTO "League" ("Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name")
VALUES (2, NULL, '0001-01-01 00:00:00', NULL, '0001-01-01 00:00:00', 'English Premiere League');
SELECT changes();

INSERT INTO "League" ("Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name")
VALUES (3, NULL, '0001-01-01 00:00:00', NULL, '0001-01-01 00:00:00', 'La Liga');
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250919112844_SeededLeagues', '9.0.8');

COMMIT;

