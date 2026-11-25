# Build Stage
FROM node:20-slim AS builder
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

# Production Stage
FROM node:20-slim AS production
WORKDIR /app
# The TanStack Start Nitro build bundles its own dependencies directly into the .output directory
COPY --from=builder /app/.output ./
# Set production environment
ENV NODE_ENV=production
EXPOSE 3000
# The production command for TanStack Start
CMD ["node", ".output/server/index.mjs"]
