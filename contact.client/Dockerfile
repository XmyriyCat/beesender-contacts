# Use node image for build and runtime
FROM node:18

WORKDIR /app

# Set React environment variable at build time
ENV REACT_APP_API_BASE_URL=http://localhost:8080/api

# Install app dependencies
COPY package*.json ./
RUN npm install

# Copy app source code
COPY . .

# Build React app
RUN npm run build

# Install 'serve' to serve the build folder
RUN npm install -g serve

# Expose port where app will run
EXPOSE 3000

# Command to serve the build directory
CMD ["serve", "-s", "build", "-l", "3000"]
