const API_URL = 'https://localhost:7261/api';

export type LoginData = {
  username: string;
  password: string;
};

export type RegisterData = {
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  address: string;
  dateofbirth: Date;
};

export type UserData = {
  username: string;
  firstName: string;
  lastName: string;
  address: string;
  dateOfBirth: Date;
};

export type Author = {
  id: number;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  fullName?: string;
};

export type Book = {
  id: number;
  title: string;
  publicationYear: number;
  genre: string;
  author?: Author;
  authorId: number;
};

export type ApiResponse<T> = {
  statusCode: number;
  message: string;
  data?: T;
  errors: string[];
};

function generateHeaders() {
  var headers: { [headerName: string]: string } = {};
  headers['Content-Type'] = 'application/json';
  headers['Origin'] = 'http://localhost:3000';

  if (localStorage.getItem('jwt')) {
    const jwt = JSON.parse(localStorage.getItem('jwt') ?? '');
    headers['Authorization'] = `Bearer ${jwt}`;
  }

  return headers;
}

async function makeRequest<T>(path: string, method: string, body?: object) {
  try {
    const response = await fetch(`${API_URL}/${path}`, {
      method: method,
      headers: generateHeaders(),
      body: JSON.stringify(body),
    });

    return (await response.json()) as ApiResponse<T>;
  } catch (error) {
    console.error('Error during making request:', error);
    return {
      statusCode: 400,
      message: `Error during making request: ${error}`,
      errors: [`Error during making request: ${error}`],
    };
  }
}

export const login = (loginData: LoginData) =>
  makeRequest<string>('auth/login', 'POST', loginData);

export const register = (registerData: RegisterData) =>
  makeRequest('auth/register', 'POST', registerData);

export const getUser = async () => {
  const result = await makeRequest<UserData>('auth/user', 'GET');

  if (result?.data != null) {
    result.data.dateOfBirth = new Date(result.data.dateOfBirth);
  }

  return result;
};

export const getBooks = async () => {
  const result = await makeRequest<Book[]>('book', 'GET');

  if (result?.data != null) {
    const books = result.data;

    for (let i in books) {
      const author = books[i].author;

      if (author != null) {
        author.dateOfBirth = new Date(author.dateOfBirth);
      }
    }
  }

  return result;
};

export const updateBook = (book: Book) => makeRequest('book', 'PUT', book);

export const createBook = (book: Book) => makeRequest('book', 'POST', book);

export const deleteBook = (id: number) => makeRequest(`book/${id}`, 'DELETE');

export const getBook = async (id: number) => {
  const result = await makeRequest<Book>(`book/${id}`, 'GET');

  if (result?.data?.author != null) {
    result.data.author.dateOfBirth = new Date(result.data.author.dateOfBirth);
  }

  return result;
};

export const getAuthors = async () => {
  const result = await makeRequest<Author[]>('author', 'GET');

  if (result?.data != null) {
    const authors = result.data;

    for (let i in authors) {
      authors[i].dateOfBirth = new Date(authors[i].dateOfBirth);
    }
  }

  return result;
};

export const updateAuthor = (author: Author) =>
  makeRequest('author', 'PUT', author);

export const createAuthor = (author: Author) =>
  makeRequest('author', 'POST', author);

export const deleteAuthor = (id: number) =>
  makeRequest(`author/${id}`, 'DELETE');

export const getAuthor = async (id: number) => {
  const result = await makeRequest<Author>(`author/${id}`, 'GET');

  if (result?.data != null) {
    result.data.dateOfBirth = new Date(result.data.dateOfBirth);
  }

  return result;
};
