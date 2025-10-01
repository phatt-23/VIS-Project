-- Insert users
INSERT INTO z_user (username, email, password) VALUES
                                                   ('alice', 'alice@example.com', 'pass123'),
                                                   ('bob', 'bob@example.com', 'pass456'),
                                                   ('charlie', 'charlie@example.com', 'pass789');

-- Insert repositories
INSERT INTO z_repository (owner_id, name, description, is_public) VALUES
                                                                      (1, 'alice-repo', 'Alice''s personal repository', 1),
                                                                      (2, 'bob-utils', 'Utility scripts and tools by Bob', 1),
                                                                      (3, 'charlie-secret', 'Private repo for Charlie', 0);

-- Insert commits
INSERT INTO z_commit (repository_id, message) VALUES
                                                  (1, 'Initial commit'),
                                                  (1, 'Add README file'),
                                                  (2, 'First commit with utilities'),
                                                  (3, 'Private initial commit');

-- Insert files (keep short content since limited to NVARCHAR(255))
INSERT INTO z_file (commit_id, path, content) VALUES
                                                  (1, 'main.py', 'print("Hello, world!")'),
                                                  (2, 'README.md', '# Alice Repo'),
                                                  (3, 'utils.sh', 'echo "Running utilities..."'),
                                                  (4, 'secret.txt', 'Top secret content');

-- Insert issues
INSERT INTO z_issue (repository_id, title, description, created_by, status) VALUES
                                                                                (1, 'Bug in main.py', 'The script crashes on invalid input.', 2, 'open'),
                                                                                (2, 'Add documentation', 'We need better docs for utilities.', 1, 'open'),
                                                                                (3, 'Security review', 'Review private repo for vulnerabilities.', 3, 'closed');

-- Insert comments
INSERT INTO z_comment (issue_id, author_id, content) VALUES
                                                         (1, 1, 'I will look into this.'),
                                                         (1, 2, 'Thanks, please fix it soon.'),
                                                         (2, 3, 'I can help with the docs.'),
                                                         (3, 2, 'The repo looks fine now.');