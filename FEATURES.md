# Features

## Task List


# Implementation Roadmap: Phase 2 (UI & Integration)
- [x] Task 1: Configure TanStack Start to proxy `/api` requests to the .NET backend during local development (Vite config).
- [x] Task 2: Build the public frontend UI in `apps/web`: Homepage, About page, and a `/posts` route that fetches published blogs from the .NET API.
- [x] Task 3: Build the `/book` route and embed the Cal.diy booking iframe.
- [x] Task 4: Audit the existing backend authentication code to ensure it uses HttpOnly cookies for the JWT. Then, build the Admin Login page in TanStack Start and wire it to the API.
- [x] Task 5: Build the protected Admin Dashboard containing a Shadcn data table for managing posts and a Tiptap rich-text editor for writing them.
- [x] Task 6: Add the .NET Health Check middleware and update the Caddyfile with a `health_uri` check.