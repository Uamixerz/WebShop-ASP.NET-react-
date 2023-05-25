export interface ICategoryCreate {
    name: string,
    image: File | null,
    description: string,
    priority: number,
    parentId: number
}
export interface ICategoryCreateError {
    name: string,
    description: string,
    image: File | string,
    priority: number
}