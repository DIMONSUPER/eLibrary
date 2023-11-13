import AuthorDetails from '@/components/AuthorDetails';

export default function Page({params}:{params:{authorId:number}}) {
  return (
    <AuthorDetails authorId={params.authorId}/>
  )
}