-- Initialises the avatars bucket

insert into storage.buckets
  (id, name, public)
values
  ('avatars', 'avatars', true);
