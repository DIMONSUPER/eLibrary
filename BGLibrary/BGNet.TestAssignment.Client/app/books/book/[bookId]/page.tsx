import BookDetails from '@/components/book/BookDetails';

export default function Page({ params }: { params: { bookId: number } }) {
  return <BookDetails bookId={params.bookId} />;
}
