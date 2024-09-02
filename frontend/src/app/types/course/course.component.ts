export interface Course {
  courseId: number;
  title: string;
  description: string;
  level: string;
  category: string;
  price: number;
  isPublished: boolean;
  professorId?: number; 
  duration: string;
  statistics?: CourseStatistics; 
}

export interface CourseStatistics {
  enrollmentsCount: number;
  averageRating: number;
}