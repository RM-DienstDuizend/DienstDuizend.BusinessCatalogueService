CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Businesses" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text,
    "KvkNumber" text NOT NULL,
    "BusinessEmail" text,
    "WebsiteUri" text,
    "LogoUri" text,
    "BannerUri" text,
    "UserId" uuid NOT NULL,
    CONSTRAINT "PK_Businesses" PRIMARY KEY ("Id")
);

CREATE TABLE "Services" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "Description" text,
    "BusinessId" uuid NOT NULL,
    "Price" numeric NOT NULL,
    "EstimatedDurationInMinutes" integer NOT NULL,
    "IsHomeService" boolean NOT NULL,
    "IsPubliclyVisible" boolean NOT NULL,
    CONSTRAINT "PK_Services" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Services_Businesses_BusinessId" FOREIGN KEY ("BusinessId") REFERENCES "Businesses" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Services_BusinessId" ON "Services" ("BusinessId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240524130634_Initial', '8.0.4');

COMMIT;

