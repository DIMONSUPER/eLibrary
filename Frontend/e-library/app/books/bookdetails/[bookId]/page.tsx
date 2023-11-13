import BookDetails from '@/components/BookDetails'

export default function Page({params}:{params:{bookId:number}}) {
  
  return (
    <BookDetails bookId={params.bookId}/>
  )
}