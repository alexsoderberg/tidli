# use the official Node.js image
FROM node:20-alpine AS base
WORKDIR /usr/src/app

# install dependencies into temp directory
# this will cache them and speed up future builds
FROM base AS install
RUN mkdir -p /temp/dev
COPY package.json package-lock.json /temp/dev/
RUN cd /temp/dev && npm ci

# install with --production (exclude devDependencies)
RUN mkdir -p /temp/prod
COPY package.json package-lock.json /temp/prod/
RUN cd /temp/prod && npm ci --only=production

# copy node_modules from temp directory
# then copy all (non-ignored) project files into the image
FROM base AS prerelease
COPY --from=install /temp/dev/node_modules node_modules
COPY . .

# [optional] tests & build
ENV NODE_ENV=production
RUN npm run build

# copy production dependencies and built app into final image
FROM node:20-alpine AS release
WORKDIR /app

# Copy only production dependencies
COPY --from=install /temp/prod/node_modules node_modules

# Copy built application
COPY --from=prerelease /usr/src/app/dist ./dist

# Copy package files
COPY --from=prerelease /usr/src/app/package.json ./package.json

# Set environment variables
ENV NODE_ENV=production
ENV PORT=3000

# Install curl for health checks
RUN apk add --no-cache curl

# Create non-root user for security
RUN addgroup -g 1001 -S nodejs
RUN adduser -S nodejs -u 1001

# Change ownership of the app directory
RUN chown -R nodejs:nodejs /app
USER nodejs

# Expose port and start the server
EXPOSE 3000
CMD ["node", "dist/web-ui/server/server.mjs"]
