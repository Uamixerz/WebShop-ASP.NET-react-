export interface ICategoryEdit {
    id: number,
    name: string,
    ImageUpload: File | null,
    description: string,
    priority: number,
    parentId: number
}
