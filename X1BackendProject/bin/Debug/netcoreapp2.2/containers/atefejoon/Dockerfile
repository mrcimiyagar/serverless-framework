FROM node:10
WORKDIR /atefejoon
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
