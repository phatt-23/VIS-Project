PRAGMA foreign_keys = ON;

-- Table: z_user
CREATE TABLE z_user (
                        user_id     INTEGER PRIMARY KEY AUTOINCREMENT,
                        username    NVARCHAR(255) NOT NULL UNIQUE,
                        email       NVARCHAR(255) NOT NULL UNIQUE,
                        password    NVARCHAR(255) NOT NULL
);

-- Table: z_repository
CREATE TABLE z_repository (
                              repository_id INTEGER PRIMARY KEY AUTOINCREMENT,
                              owner_id      INTEGER NOT NULL,
                              name          NVARCHAR(255) NOT NULL,
                              description   NVARCHAR(1000),
                              is_public     INTEGER NOT NULL DEFAULT 1, -- SQLite doesn't have BIT, use INTEGER 0/1
                              created_at    DATETIME DEFAULT CURRENT_TIMESTAMP,
                              FOREIGN KEY (owner_id) REFERENCES z_user(user_id) ON DELETE CASCADE
);

-- Table: z_commit
CREATE TABLE z_commit (
                          commit_id     INTEGER PRIMARY KEY AUTOINCREMENT,
                          repository_id INTEGER NOT NULL,
                          message       NVARCHAR(255) NOT NULL,
                          created_at    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                          FOREIGN KEY (repository_id) REFERENCES z_repository(repository_id) ON DELETE CASCADE
);

-- Table: z_file
CREATE TABLE z_file (
                        file_id   INTEGER PRIMARY KEY AUTOINCREMENT,
                        commit_id INTEGER NOT NULL,
                        path      NVARCHAR(255) NOT NULL,
                        content   NVARCHAR(255) NOT NULL,
                        FOREIGN KEY (commit_id) REFERENCES z_commit(commit_id) ON DELETE CASCADE
);

-- Enum simulation: issue_status ('open', 'closed')
-- In SQLite we use TEXT with a CHECK constraint
CREATE TABLE z_issue (
                         issue_id     INTEGER PRIMARY KEY AUTOINCREMENT,
                         repository_id INTEGER NOT NULL,
                         creator_id   INTEGER NOT NULL,
                         title        NVARCHAR(255) NOT NULL,
                         description  NVARCHAR(1000),
                         status       TEXT NOT NULL DEFAULT 'open' CHECK (status IN ('open', 'closed')),
                         created_at   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                         closed_at    DATETIME NULL DEFAULT NULL,
                         FOREIGN KEY (repository_id) REFERENCES z_repository(repository_id) ON DELETE CASCADE,
                         FOREIGN KEY (creator_id) REFERENCES z_user(user_id) ON DELETE CASCADE,
);

-- Table: z_comment
CREATE TABLE z_comment (
                           comment_id INTEGER PRIMARY KEY AUTOINCREMENT,
                           issue_id   INTEGER NOT NULL,
                           author_id  INTEGER NOT NULL,
                           content    NVARCHAR(1000) NOT NULL,
                           created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                           FOREIGN KEY (issue_id) REFERENCES z_issue(issue_id) ON DELETE CASCADE,
                           FOREIGN KEY (author_id) REFERENCES z_user(user_id) ON DELETE CASCADE
);
